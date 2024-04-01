import { Review } from "@/types"
import ReviewRating from "./review-rating"

interface Props {
    review: Review
}

export default function ReviewCard({ review }: Props) {
    return (
        <div className="bg-secondary flex-grow bg-opacity-10 p-4 rounded-lg shadow-3xl">
            <div className="bg-header bg-opacity-75 p-4 rounded-lg">
                <div className="flex justify-between items-center mb-2">
                    <h2 className="text-2xl font-semibold text-third">{review.reviewerName}</h2>
                    <ReviewRating value={review.rating} readonly={true} />
                </div>
                <p className="text-gray-700 cursor-pointer">
                    {review.comment}
                </p>
                <p className="text-right text-xs font-semibold text-third">Posted on {new Date(review.createdOnDate).toLocaleString()}</p>
            </div>
        </div>
    )
}