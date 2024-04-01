'use server'

import { fetchWrapper } from "@/lib/fetchWrapper"
import { Genre } from "@/types";
import { FieldValues } from "react-hook-form";

export async function getGenre(id: string): Promise<Genre> {
    return await fetchWrapper.get(`/api/genres/${id}`)
}

export async function getGenres(): Promise<Genre[]> {
    return await fetchWrapper.get(`/api/genres`);
}

export async function updateGenre(data: FieldValues) {
    return await fetchWrapper.put(`/api/genres`, data)
}

export async function createGenre(data: FieldValues) {
    return await fetchWrapper.post(`/api/genres`, data);
}

export async function deleteGenre(id: string) {
    return await fetchWrapper.del(`/api/genres/${id}`)
}