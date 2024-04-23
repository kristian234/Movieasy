'use server'

import { fetchWrapper } from "@/lib/fetchWrapper";
import { Profile } from "@/types";
import { FieldValues } from "react-hook-form";

export async function getProfile(profileId: string): Promise<Profile>  {
    return await fetchWrapper.get(`/api/profiles/${profileId}`);
}

export async function updateProfile(data: FieldValues){
    return await fetchWrapper.put('/api/profiles', data);
}