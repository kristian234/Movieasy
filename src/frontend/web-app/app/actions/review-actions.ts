'use server'

import { fetchWrapper } from "@/lib/fetchWrapper";
import { DetailedReviewData, PagedResult, Review } from "@/types";
import { FieldValues } from "react-hook-form";

export async function createReview(data: FieldValues) {
    return await fetchWrapper.post(`/api/Reviews`, data)
}

export async function getReviews(query: string): Promise<PagedResult<Review>> {
    return await fetchWrapper.get(`/api/Reviews${query}`)
}

export async function getDetailedReviewData(id: string): Promise<DetailedReviewData> {
    return await fetchWrapper.get(`/api/Reviews/summary/${id}`);
}

export async function updateReview(data: FieldValues) {
    return await fetchWrapper.put(`/api/Reviews`, data);
}

export async function getUserReviewForMovie(movieId: string) {
    return await fetchWrapper.get(`/api/Reviews/user/${movieId}`)
}

export async function getUserLatestReview(userId: string) {
    return await fetchWrapper.get(`/api/Reviews?userId=${userId}&pageSize=1&sortTerm=newest`)
}


export async function deleteReview(movieId: string) {
    return await fetchWrapper.del(`/api/reviews/${movieId}`);
}