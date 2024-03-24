'use server'

import { getServerSession } from "next-auth";
import { getToken } from "next-auth/jwt";
import { cookies, headers } from 'next/headers';
import { NextApiRequest } from "next";
import { authOptions } from "../api/auth/[...nextauth]/route";
import { fetchWrapper } from "@/lib/fetchWrapper";

export async function getSession() {
    return await getServerSession(authOptions);
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

export async function getLoggedOut() {
    try {
        const session = await getSession();

        if (!session) return null;

        return session.error
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