import { Movie, PagedResult } from "@/types";

export async function getData(pageNumber: number = 1) : Promise<PagedResult<Movie>>{
    const res = await fetch(`https://localhost:65209/api/Movies?page=1&pageSize=4`)

    if(!res) throw new Error('Failed to fetch data');

    return await res.json();
}