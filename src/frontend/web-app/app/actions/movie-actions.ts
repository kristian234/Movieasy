import { Movie, PagedResult } from "@/types";

export async function getData(query: string) : Promise<PagedResult<Movie>>{
    const res = await fetch(`https://localhost:53781/api/Movies${query}`,
    {mode: 'no-cors'})
    
    if(!res) throw new Error('Failed to fetch data');

    return await res.json();
}