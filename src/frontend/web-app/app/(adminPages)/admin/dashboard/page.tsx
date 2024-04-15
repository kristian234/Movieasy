'use client'

import { getMovieStatistic, getReviewStatistic } from "@/app/actions/statistic-actions";
import LoadingComponent from "@/app/components/shared/loading-component";
import { Statistic } from "@/types";
import { useState, useEffect } from "react";
import { toast } from "react-toastify";

export default function AdminDashboardPage() {
    const [totalMovies, setTotalMovies] = useState(0);
    const [totalReviews, setTotalReviews] = useState(0);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fetchData() {
            try {
                const movieStatistic = await getMovieStatistic();
                if ((movieStatistic as any).error) {
                    toast.error("Failed to retrieve movie statistic");
                } else {
                    setTotalMovies(movieStatistic.total);
                }

                const reviewStatistic = await getReviewStatistic();
                if ((reviewStatistic as any).error) {
                    toast.error("Failed to retrieve review statistic");
                } else {
                    setTotalReviews(reviewStatistic.total);
                }

                setLoading(false);
            } catch (error) {
                console.error('Error fetching statistics:', error);
                setLoading(false);
            }
        }
        fetchData();
    }, []);

    return (
        <div className="flex flex-col justify-center items-center h-screen">
            {loading ? (
                <LoadingComponent />
            ) : (
                <div className="flex flex-col items-center">
                    <div className="text-5xl text-secondary font-semibold mb-4">Statistic:</div>
                    <div className="flex flex-col sm:flex-row justify-center items-center">
                        <div className="w-full sm:w-auto rounded-lg overflow-hidden shadow-3xl bg-secondary mx-4 my-2 sm:my-0">
                            <div className="p-6">
                                <h2 className="text-xl font-semibold text-header mb-2">Total Movies:</h2>
                                <p className="text-2xl font-bold text-darkHeader">{totalMovies}</p>
                            </div>
                        </div>
                        <div className="w-full sm:w-auto rounded-lg overflow-hidden bg-secondary shadow-3xl mx-4 my-2 sm:my-0">
                            <div className="p-6">
                                <h2 className="text-xl font-semibold text-header mb-2">Total Reviews:</h2>
                                <p className="text-2xl font-bold text-headerDark">{totalReviews}</p>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}