'use server'

import { Movie, PagedResult } from "@/types";
import { getTokenWorkaround } from "./auth-actions";

export async function getData(query: string) : Promise<PagedResult<Movie>>{
    const token = await getTokenWorkaround();
    const res = await fetch(process.env.URL + `/api/Movies${query}`,
    {
        headers:{
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token?.accessToken
        }
    })
    if(!res) throw new Error('Failed to fetch data');

    return res.json();
}