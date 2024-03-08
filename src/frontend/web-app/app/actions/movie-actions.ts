'use server'

import { Movie, PagedResult } from "@/types";
import { getTokenWorkaround } from "./auth-actions";

export async function getData(query: string) : Promise<PagedResult<Movie>>{
    const token = await getTokenWorkaround();
    console.log("niggaballs" + token?.accessToken);
    const res = await fetch(`https://localhost:54759/api/Movies${query}`,
    {
        headers:{
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token?.accessToken
        }
    })
    const reso = await res.json();

    if(!res) throw new Error('Failed to fetch data');

    return reso
}