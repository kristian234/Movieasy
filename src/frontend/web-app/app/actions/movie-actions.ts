'use server'

import { Movie, PagedResult } from "@/types";
import { fetchWrapper } from "@/lib/fetchWrapper";
import { FieldValues } from "react-hook-form";

export async function getData(query: string): Promise<PagedResult<Movie>> {
    return await fetchWrapper.get(`/api/Movies${query}`);
}

export async function getDetailedData(id: string) : Promise<Movie>{
    return await fetchWrapper.get(`/api/Movies/${id}`);
}

export async function getRecent(): Promise<PagedResult<Movie>> {
    return await fetchWrapper.get(`/api/Movies?pageNumber=1&pageSize=12&sortColumn=upload&sortOrder=desc`)
}

export async function getTrending(): Promise<PagedResult<Movie>> {
    return await fetchWrapper.get(`/api/Movies?pageNumber=1&pageSize=12`)
}

export async function createMovie(data: FormData){
    console.log("9GJEA9GEAJ9GAEJHG");
    return await fetchWrapper.postForm('/api/Movies', data);
}