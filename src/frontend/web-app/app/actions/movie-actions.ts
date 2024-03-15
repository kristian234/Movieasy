'use server'

import { Movie, PagedResult } from "@/types";
import { fetchWrapper } from "@/lib/fetchWrapper";

export async function getData(query: string): Promise<PagedResult<Movie>> {
    return await fetchWrapper.get(`/api/Movies${query}`);
}

export async function getRecent(): Promise<PagedResult<Movie>> {
    return await fetchWrapper.get(`/api/Movies?pageNumber=1&pageSize=12`)
}