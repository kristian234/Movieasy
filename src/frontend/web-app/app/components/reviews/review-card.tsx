import { Review } from "@/types"
import ReviewRating from "./review-rating"
import { useState } from "react"
import { deleteReview } from "@/app/actions/review-actions"
import { toast } from "react-toastify"
import { Button } from "flowbite-react"
import Link from "next/link"

interface Props {
    review: Review
    isAdmin?: boolean | null
    onDelete: (reviewId: string) => void; // callback
}

export default function ReviewCard({ review, isAdmin = false, onDelete }: Props) {
    const [showFullComment, setShowFullComment] = useState(false);
    const [isDeleting, setIsDeleting] = useState(false);

    const toggleComment = () => {
        setShowFullComment(!showFullComment);
    }

    const handleDelete = async (reviewId: string) => {
        setIsDeleting(true);
        const res = await deleteReview(reviewId);

        if (res.error) {
            toast.error(res.error);
            setIsDeleting(false);
            return;
        }

        toast.success("Successfully deleted review");
        setIsDeleting(false);
        onDelete(reviewId);
    }

    return (
        <div className="bg-secondary flex-grow bg-opacity-10 p-4 rounded-lg shadow-3xl relative">
            <div className="bg-header bg-opacity-75 p-4 rounded-lg flex flex-col">
                <div className="flex justify-between items-center mb-2">
                    <Link href={`/profiles/details/${review.userId}`}>
                    <h2 className="text-2xl font-semibold text-third cursor-pointer hover:text-secondary hover:scale-105 transition-transform duration-300">{review.reviewerName}</h2>
                    </Link>
                    <ReviewRating value={review.rating} readonly={true} />
                </div>
                <p className={`text-gray-700 cursor-pointer break-words ${showFullComment ? 'overflow-y-auto' : 'overflow-hidden'}`} onClick={toggleComment}>
                    {showFullComment ? review.comment : review.comment.length > 100 ? `${review.comment.slice(0, 100)}...` : review.comment}
                </p>
                <div className="flex justify-end items-center">
                    {isAdmin && (
                        <Button
                            color='failure'
                            isProcessing={isDeleting}
                            className="modal-action"
                            onClick={() => handleDelete(review.id)}
                        >Delete</Button>
                    )}
                    <p className="text-xs font-semibold text-third">Posted on {new Date(review.createdOnDate).toLocaleDateString()}</p>
                </div>
            </div>
        </div>
    )
}