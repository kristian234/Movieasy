'use server'

import { fetchWrapper } from "@/lib/fetchWrapper";
import { NextApiRequest } from "next";
import { getServerSession } from "next-auth";
import { getToken } from "next-auth/jwt";
import { cookies, headers } from "next/headers";

export async function registerUser(
    firstName: string,
    lastName: string,
    email: string,
    password: string): Promise<any> {

    const res = await fetchWrapper.post("/api/user/register", {
        email: email,
        password: password,
        firstName: firstName,
        lastName: lastName
    })

    return res;
}

export async function getSession() {
    return await getServerSession();
}

export async function getCurrentUser() {
    try {
        const session = await getSession();

        if (!session) return null;

        return session.user
    } catch (error) {
        return null;
    }
}

export async function getTokenWorkaround() {
    const req = {
        headers: Object.fromEntries(headers() as Headers),
        cookies: Object.fromEntries(cookies().getAll().map(c => [c.name, c.value]))
    } as NextApiRequest;

    return await getToken({ req });
}