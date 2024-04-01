'use server'

import { fetchWrapper } from "@/lib/fetchWrapper";
import { FieldValues } from "react-hook-form";

export async function createReview(data: FieldValues) {
    return await fetchWrapper.post(`/api/Reviews`, data)
}