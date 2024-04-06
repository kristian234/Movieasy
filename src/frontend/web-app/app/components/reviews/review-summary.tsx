'use client'

import { DetailedReviewData } from "@/types";
import ReviewRating from "./review-rating";

interface Props {
    detailedSummary: DetailedReviewData | null
}

export default function ReviewSummary({ detailedSummary }: Props) {
    return (
        <div className="">
            <div className="text-center">
                <h1 className="font-semibold text-2xl text-secondary justify-center items-center">{detailedSummary?.averageRating.toFixed(2) || 'None'}</h1>
            </div>
            <div className="text-center">
                <ReviewRating readonly={true} value={detailedSummary?.averageRating || 0} />
                <h2 className="font-semibold text-1xl text-secondary">{detailedSummary?.totalRatings ? `${detailedSummary.totalRatings} total reviews` : 'No reviews'}</h2>
            </div>
        </div>
    )
}
