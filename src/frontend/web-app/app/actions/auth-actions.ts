'use server'

import axios, { AxiosResponse } from "axios";
import { NextApiRequest } from "next";
import { getServerSession } from "next-auth";
import { getToken } from "next-auth/jwt";
import { cookies, headers } from "next/headers";

export async function registerUser(
    firstName: string,
    lastName: string,
    email: string,
    password: string) : Promise<number> {

    const res = await axios.post(process.env.URL + "/api/user/register", {
        email: email,
        password: password,
        firstName: firstName,
        lastName: lastName
    }, {
        withCredentials: true,
        headers: {
            "Content-Type": "application/json"
        }
    });

    return res.status;
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