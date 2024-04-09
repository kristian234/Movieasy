'use client'

import { DetailedReviewData, PagedResult, Review } from "@/types";
import { Fragment, useEffect, useState } from "react";
import qs from 'query-string'
import { getDetailedReviewData, getReviews } from "@/app/actions/review-actions";
import { toast } from "react-toastify";
import ReviewCard from "./review-card";
import ReviewFilters from "./review-filter";
import EmptyReviewFilter from "./empty-review-filter";
import AppPagination from "../shared/app-pagination";
import { Spinner } from "flowbite-react";
import ReviewSummary from "./review-summary";
import { IoRefresh } from "react-icons/io5";
interface Props {
    movieId: string
}

export default function ReviewListing({ movieId }: Props) {
    const [data, setData] = useState<PagedResult<Review>>();
    const [detailedReviewData, setDetailedReviewData] = useState<DetailedReviewData | null>(null); // State for review summary data

    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [params, setParams] = useState<{
        page: number,
        pageSize: number,
        rating: number | null
        sortTerm: 'newest' | 'oldest' | null
    }>({
        page: 1,
        pageSize: 12,
        rating: null,
        sortTerm: null
    });


    const handleFilterChange = (rating: number | null) => {
        if (rating == 0) {
            rating = null;
        }

        setParams(prevParams => ({ ...prevParams, rating }));
    };

    function setPageNumber(pageNumber: number) {
        setParams(prevParams => ({ ...prevParams, page: pageNumber }));
    }

    useEffect(() => {
        const fetchReviews = async () => {
            const url = qs.stringifyUrl({ url: '', query: { ...params, movieId } })

            const reviews = await getReviews(url);
            const overallReviewData = await getDetailedReviewData(movieId);

            if ((reviews as any).error || (overallReviewData as any).error) {
                setIsLoading(false);
                return;
            }

            setDetailedReviewData(overallReviewData);
            setData(reviews);
            setIsLoading(false);
        }

        fetchReviews();
    }, [params])

    const resetFilters = () => {
        setParams({
            page: 1,
            pageSize: 12,
            rating: null,
            sortTerm: null
        });
    };

    const handleSortChange = (orderBy: 'newest' | 'oldest' | 'none') => {
        const sortTerm: 'newest' | 'oldest' | null = orderBy === 'none' ? null : orderBy;
        setParams(prevParams => ({ ...prevParams, sortTerm }));
    };

    const handleRefresh = async () => {
        const url = qs.stringifyUrl({ url: '', query: { ...params, movieId } })

        const reviews = await getReviews(url);
        const overallReviewData = await getDetailedReviewData(movieId);

        if ((reviews as any).error || (overallReviewData as any).error) {
            setIsLoading(false);
            return;
        }

        setDetailedReviewData(overallReviewData);
        setData(reviews);
        setIsLoading(false);
    };

    return (
        <div className="relative">
            <div className="flex sm:flex-row flex-col justify-between mt-8 mx-auto max-w-full px-8 w-[900px] sm:px-8 items-center">
                <ReviewSummary detailedSummary={detailedReviewData} />
                <div className="flex justify-center items-center mt-2">
                    <button onClick={handleRefresh} className="bg-transparent mr-2 text-secondary hover:bg-third font-bold py-2 px-2 rounded-3xl text-3xl">
                        <IoRefresh />
                    </button>
                </div>
                <div className="flex justify-center  items-center mt-2 ">
                    <ReviewFilters onSortChange={handleSortChange} onFilterChange={handleFilterChange} />
                </div>
            </div>

            {isLoading ? (
                <div className="flex justify-center items-center mt-10">
                    <Spinner />
                </div>) : data?.totalPages === 0 ? (
                    <EmptyReviewFilter reset={resetFilters} />
                ) : (
                <>
                    <div className="flex flex-col justify-center p-6 mx-auto max-w-full px-4 w-[900px] sm:px-8">
                        <div className="w-6xl flex flex-col space-y-4">
                            {data?.items?.map((review, index) => (
                                <Fragment>
                                    <ReviewCard review={review} key={index} />
                                </Fragment>
                            ))}
                        </div>
                    </div>
                </>
            )}

            <div className="flex justify-center p-3">
                <AppPagination currentPage={params.page} pageCount={data?.totalPages || 0} pageChanged={setPageNumber} />
            </div>
        </div>
    )
}