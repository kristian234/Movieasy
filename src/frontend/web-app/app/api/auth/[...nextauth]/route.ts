import NextAuth from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";
import axios from 'axios';
import { jwtDecode } from "jwt-decode";
import { parse } from "cookie";
import { getIsTokenValid } from "./helper";
import { JWT } from "next-auth/jwt";
import { signOut } from "next-auth/react";

interface TokenDecode {
    email: string,
    name: string,
    sid: string,
    sub: string,
    exp: number,
}

export const handler = NextAuth({
    providers: [
        CredentialsProvider({
            credentials: {
                email: {},
                password: {},
            },
            id: 'credential',
            async authorize(credentials, req) {
                const rememberMeCookie = parse(req?.headers?.cookie)['rememberMe'] === 'true' ? true : false;

                const res = await axios.post(process.env.URL + "/api/user/login", {
                    email: credentials?.email,
                    password: credentials?.password,
                    rememberMe: rememberMeCookie
                }, {
                    withCredentials: true,
                    headers: {
                        "Content-Type": "application/json"
                    }
                });

                if (res.status == 200) {
                    const decodedToken = jwtDecode<TokenDecode>(res.data.accessToken);

                    return {
                        id: decodedToken.sub,
                        accessToken: res.data.accessToken,
                        refreshToken: res.data.refreshToken,
                        accessTokenExpiry: decodedToken.exp
                    }
                }

                return null;
            }
        })
    ],

    session: {
        strategy: "jwt",
        maxAge: 3000
    },

    jwt:{

    },

    pages: {
        signIn: "/auth/login"
    },

    callbacks: {
        jwt: async ({ token, profile, account, user, trigger }) => {
            if (user) {
                console.log("WT9AEG9AEJ9GAE9JGAJE9HAEHAE9GAEJ9HJAE");

                return {
                    ...token,
                    id: user.id,
                    accessToken: user.accessToken,
                    refreshToken: user.refreshToken,
                    accessTokenExpiry: user.accessTokenExpiry
                }
            }

            const dateNowInSeconds = new Date().getTime() / 1000;

            if (dateNowInSeconds < Number(token.accessTokenExpiry)) {
                return token;
            }

            return refreshAccessToken(token);
        },

        async session({ session, token }) {
            const isTokenValid = getIsTokenValid(token);
            if (!isTokenValid) {
                session.user.email = null;
                session.user.id = null;
                session.user.name = null;
                session.error = "RefreshAccessTokenError"

                return { ...session, token }
            };

            if (token) {
                const res = await axios.get(process.env.URL + "/api/user/me",
                    {
                        headers: {
                            "Content-Type": "application/json",
                            "Authorization": `Bearer ${token.accessToken}`
                        }
                    });

                if (res.status == 200) {
                    session.user.email = res.data.email,
                        session.user.name = `${res.data.firstName} ${res.data.lastName}`,
                        session.user.id = res.data.id
                }
            }

            return { ...session, token: token };
        },


    }
})


async function refreshAccessToken(token: JWT) {
    console.log("Refreshing access token");
    try {
        if (!token.refreshToken) {
            return { ...token, error: "RefreshAccessTokenError" as const } 
        }

        const response = await axios.post(process.env.URL + "/api/user/refresh", {
            refreshToken: token.refreshToken,
        }, {
            withCredentials: true,
            headers: {
                "Content-Type": "application/json"
            }
        });

        const refreshedTokens = response.data;

        if (response.status != 200) {
            throw refreshedTokens;
        }

        console.log("Refreshed access token");
        console.log(refreshedTokens);

        const decodedToken = jwtDecode<TokenDecode>(response.data.accessToken);

        token = {
            ...token,
            accessToken: response.data.accessToken,
            refreshToken: response.data.refreshToken,
            accessTokenExpiry: decodedToken.exp,
            id: decodedToken.sid,
        }

        return token;
    } catch (error) {
        console.log("ERROR ERROR ERROR ERROR " + error);

        return { ...token, token: undefined, error: "RefreshAccessTokenError" as const }
    }
}

export { handler as GET, handler as POST }
