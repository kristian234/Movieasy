'use server'

import { Statistic } from "@/types";
import { fetchWrapper } from "@/lib/fetchWrapper";

export async function getMovieStatistic(): Promise<Statistic> {
    return await fetchWrapper.get(`/api/statistics/movies`);
}

export async function getReviewStatistic(): Promise<Statistic> {
    return await fetchWrapper.get(`/api/statistics/reviews`); 2
}