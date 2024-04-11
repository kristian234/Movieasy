'use server'

import { fetchWrapper } from "@/lib/fetchWrapper";
import { Actor, PagedResult } from "@/types";
import { FieldValues } from "react-hook-form";

export async function getActor(id: string): Promise<Actor> {
    return await fetchWrapper.get(`/api/actors/${id}`)
}

export async function getData(query: string): Promise<PagedResult<Actor>> {
    return await fetchWrapper.get(`/api/actors${query}`);
}

export async function createActor(data: FieldValues){
    return await fetchWrapper.post(`/api/actors`, data);
}

export async function updateActor(data: FieldValues){
    return await fetchWrapper.put(`/api/actors`, data);
}

export async function deleteActor(id: string) {
    return await fetchWrapper.del(`/api/actors/${id}`)
}