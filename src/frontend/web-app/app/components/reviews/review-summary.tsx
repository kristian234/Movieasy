'use client'

import { getDetailedReviewData } from "@/app/actions/review-actions";
import { DetailedReviewData } from "@/types";
import { Fragment, useEffect, useState } from "react";
import { toast } from "react-toastify";
import ReviewRating from "./review-rating";

interface Props {
    movieId: string
}

export default function ReviewSummary({ movieId }: Props) {
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [data, setData] = useState<DetailedReviewData>();

    useEffect(() => {
        const fetchDetailedReviewData = async () => {
            const response = await getDetailedReviewData(movieId);

            if ((response as any).error) {
                setIsLoading(false);
                return;
            }

            setData(response);
            setIsLoading(false);
        }

        fetchDetailedReviewData();
    }, [])

    if (isLoading) {
        return <div>Loading...</div>; // Replace this with your actual loading indicator
    }

    return (
        <div className="">
            <div className="text-center">
                <h1 className="font-semibold text-2xl text-secondary justify-center items-center">{data?.averageRating.toFixed(2) || 'None'}</h1>
            </div>
            <div className="text-center">
                <ReviewRating readonly={true} value={data?.averageRating || 0} />
                <h2 className="font-semibold text-1xl text-secondary">{data?.totalRatings ? `${data.totalRatings} total reviews` : 'No reviews'}</h2>
            </div>
        </div>
    )
}
