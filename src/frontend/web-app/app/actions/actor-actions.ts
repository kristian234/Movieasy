'use server'

import { fetchWrapper } from "@/lib/fetchWrapper";
import { FieldValues } from "react-hook-form";

export async function createActor(data: FieldValues){
    return await fetchWrapper.post(`/api/actors`, data);
}

export async function updateActor(data: FieldValues){
    return await fetchWrapper.put(`/api/actors`, data);
}