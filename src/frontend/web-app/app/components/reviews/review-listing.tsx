'use client'

import { PagedResult, Review } from "@/types";
import { Fragment, useEffect, useState } from "react";
import qs from 'query-string'
import { getReviews } from "@/app/actions/review-actions";
import { toast } from "react-toastify";
import EmptyFilter from "../movies/empty-filter";
import ReviewCard from "./review-card";
import ReviewFilters from "./review-filter";

interface Props {
    movieId: string
}

export default function ReviewListing({ movieId }: Props) {
    const [data, setData] = useState<PagedResult<Review>>();
    const [params, setParams] = useState<{
        page: number,
        pageSize: number,
        rating: number | null
    }>({
        page: 1,
        pageSize: 12,
        rating: null
    });


    const handleFilterChange = (rating: number | null) => {
        setParams(prevParams => ({ ...prevParams, rating }));
    };


    useEffect(() => {
        const fetchReviews = async () => {
            const url = qs.stringifyUrl({ url: '', query: {...params, movieId} })

            const response = await getReviews(url);

            if ((response as any).error) {
                toast.error((response as any).error.message);

                return;
            }

            setData(response);
        }

        fetchReviews();
    }, [params])

    return (
        <div className="relative">
            <div className="flex flex-grow justify-end mt-8 mx-auto max-w-full px-8 w-[900px] sm:px-8 items-center">
                <ReviewFilters onFilterChange={handleFilterChange} />
            </div>
            {data?.totalPages === 0 ? (
                <EmptyFilter />
            ) : (
                <>
                    <div className="flex flex-grow flex-col justify-center p-6 mx-auto max-w-full px-8 w-[900px] sm:px-8">
                        <div className="w-6xl flex">
                            {data?.items?.map((review, index) => (
                                <Fragment>
                                    <ReviewCard review={review} key={index} />
                                </Fragment>
                            ))}
                        </div>
                    </div>
                </>
            )}
        </div>
    )
}