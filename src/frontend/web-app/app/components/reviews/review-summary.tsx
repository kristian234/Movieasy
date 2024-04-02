'use client'

import { getDetailedReviewData } from "@/app/actions/review-actions";
import { DetailedReviewData } from "@/types";
import { useEffect, useState } from "react";
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
                toast.error((response as any).error.message);
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
        <div>
            <h1 className="font-semibold text-2xl text-secondary">{data?.averageRating.toFixed(2)}</h1>
            <div>
                <ReviewRating readonly={true} value={data?.averageRating || 0} />
                <h2 className="font-semibold text-1xl text-secondary">{data?.totalRatings} total</h2>
            </div>
        </div>
    )
}
