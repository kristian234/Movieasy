import { Review } from "@/types"
import ReviewRating from "./review-rating"
import { useState } from "react"

interface Props {
    review: Review
}

export default function ReviewCard({ review }: Props) {
    const [showFullComment, setShowFullComment] = useState(false);

    const toggleComment = () => {
        setShowFullComment(!showFullComment);
    }

    return (
        <div className="bg-secondary flex-grow bg-opacity-10 p-4 rounded-lg shadow-3xl">
            <div className="bg-header bg-opacity-75 p-4 rounded-lg">
                <div className="flex justify-between items-center mb-2">
                    <h2 className="text-2xl font-semibold text-third">{review.reviewerName}</h2>
                    <ReviewRating value={review.rating} readonly={true} />
                </div>
                <p className={`text-gray-700 cursor-pointer break-words ${showFullComment ? 'overflow-y-auto' : 'overflow-hidden'}`} onClick={toggleComment}>
                    {showFullComment ? review.comment : review.comment.length > 100 ? `${review.comment.slice(0, 100)}...` : review.comment}
                </p>
                <p className="text-right text-xs font-semibold text-third">Posted on {new Date(review.createdOnDate).toLocaleDateString()}</p>
            </div>
        </div>
    )
}