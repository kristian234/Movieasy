import NextAuth, { NextAuthOptions } from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";
import axios from 'axios';
import { jwtDecode } from "jwt-decode";
import { getIsTokenValid } from "./helper";
import { JWT } from "next-auth/jwt";
import { parse } from "cookie";

interface TokenDecode {
    email: string,
    name: string,
    sid: string,
    sub: string,
    exp: number,
}

export const authOptions: NextAuthOptions = {
    providers: [
        CredentialsProvider({
            credentials: {
                email: {},
                password: {},
            },
            id: 'credential',
            async authorize(credentials, req) {
                const rememberMe = parse(req?.headers?.cookie)['rememberMe'] === 'true' ? true : false;

                const res = await axios.post(process.env.URL + "/api/user/login", {
                    email: credentials?.email,
                    password: credentials?.password,
                    rememberMe: rememberMe
                }, {
                    withCredentials: true,
                    headers: {
                        "Content-Type": "application/json"
                    }
                });

                if (res.status == 200) {
                    const userData = await axios.get(process.env.URL + "/api/user/me",
                        {
                            headers: {
                                "Content-Type": "application/json",
                                "Authorization": `Bearer ${res.data.accessToken}`
                            }
                        });

                    if (userData) {
                        const decodedToken = jwtDecode<TokenDecode>(res.data.accessToken);

                        return {
                            id: decodedToken.sub,
                            accessToken: res.data.accessToken,
                            refreshToken: res.data.refreshToken,
                            accessTokenExpiry: decodedToken.exp,
                            name: `${userData.data.firstName} ${userData.data.lastName}`,
                            croles: userData.data.roles,
                            email: userData.data.email
                        }
                    }
                }

                return null;
            }
        })
    ],

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
                    croles: user.croles,
                    accessToken: user.accessToken,
                    refreshToken: user.refreshToken,
                    accessTokenExpiry: user.accessTokenExpiry
                }
            }

            const dateNowInSeconds = new Date().getTime() / 1000;

            if (dateNowInSeconds < Number(token.accessTokenExpiry)) {
                return token;
            }

            if (token.refreshToken) {
                return refreshAccessToken(token);
            }

            return token;
        },

        async session({ session, token, trigger }) {

            const isTokenValid = getIsTokenValid(token);
            if (!isTokenValid) {
                session.user.email = null;
                session.user.id = null;
                session.user.name = null;
                session.error = "RefreshAccessTokenError"
                return { ...session, token }
            };

            if (trigger === 'update' && token) {

                const res = await axios.get(process.env.URL + "/api/user/me",
                    {
                        headers: {
                            "Content-Type": "application/json",
                            "Authorization": `Bearer ${token.accessToken}`
                        }
                    });

                console.log("GDA9GHJAD9AJD9HAEJHG9EAJGH9AEJHGA0E9 HGJAE90GJA9EGHJEA9");
                if (res.status == 200) {
                    session.user.email = res.data.email,
                        session.user.name = `${res.data.firstName} ${res.data.lastName}`,
                        session.user.id = res.data.id,
                        session.user.roles = res.data.roles
                }

            }
            if (token) {
                session.user.id = token.id,
                session.user.name = token.name,
                session.user.email = token.email,
                session.user.roles = token.croles
            }
            return { ...session, token: token };
        },

    }
}

export const handler = async (req: any, res: any) => {
    const rememberMeCookieValue = req.cookies.get('rememberMe')?.value || 'false';
    const rememberMe = rememberMeCookieValue === 'true';

    return await NextAuth(req, res, {
        ...authOptions,
        session: {
            strategy: "jwt",
            maxAge: rememberMe ? 3000 : 60
        },
    })
}


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

        return { ...token, refreshAccessToken: undefined, error: "RefreshAccessTokenError" as const }
    }
}

export { handler as GET, handler as POST }