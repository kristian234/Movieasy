'use server'

import { fetchWrapper } from "@/lib/fetchWrapper";
import { Actor, PagedResult } from "@/types";
import { FieldValues } from "react-hook-form";


export async function getData(query: string): Promise<PagedResult<Actor>> {
    return await fetchWrapper.get(`/api/actors${query}`);
}

export async function createActor(data: FieldValues){
    return await fetchWrapper.post(`/api/actors`, data);
}

export async function updateActor(data: FieldValues){
    return await fetchWrapper.put(`/api/actors`, data);
}