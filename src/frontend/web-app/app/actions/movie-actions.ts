'use server'

import { Actor, Movie, PagedResult } from "@/types";
import { fetchWrapper } from "@/lib/fetchWrapper";

export async function getData(query: string): Promise<PagedResult<Movie>> {
    return await fetchWrapper.get(`/api/Movies${query}`);
}

export async function getDetailedData(id: string) : Promise<Movie>{
    return await fetchWrapper.get(`/api/Movies/${id}`);
}

export async function getRecentlyUploaded(): Promise<PagedResult<Movie>> {
    return await fetchWrapper.get(`/api/Movies?pageNumber=1&pageSize=12&sortColumn=upload&sortOrder=desc`)
}

export async function getTrending(): Promise<PagedResult<Movie>> {
    return await fetchWrapper.get(`/api/Movies?pageNumber=1&pageSize=12&sortColumn=random`)
}

export async function createMovie(data: FormData){
    return await fetchWrapper.postForm('/api/Movies', data);
}

export async function updateMovie(data: FormData){
    return await fetchWrapper.putForm('/api/Movies', data);
}

export async function deleteMovie(movieId: string){
    return await fetchWrapper.del(`/api/Movies/${movieId}`)
}

export async function getMovieActors(movieId: string) : Promise<Actor[]>{
    return await fetchWrapper.get(`/api/movies/${movieId}/actors`);
}