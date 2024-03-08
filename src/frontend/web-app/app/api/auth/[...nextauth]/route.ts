import NextAuth from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";
import axios from 'axios';
import { jwtDecode } from "jwt-decode";

interface TokenDecode {
    email: string,
    name: string,
    sid: string,
    sub: string
}

export const handler = NextAuth({
    providers: [
        CredentialsProvider({
            credentials: {
                email: {},
                password: {}
            },
            id: 'credential',
            async authorize(credentials, req) {
                const res = await axios.post(process.env.URL + "/api/user/login", {
                    email: credentials?.email,
                    password: credentials?.password
                }, {
                    withCredentials: true,
                    headers: {
                        "Content-Type": "application/json"
                    }
                });

                if (res.status == 200) {
                    const decodedToken = jwtDecode<TokenDecode>(res.data.accessToken);

                    return {
                        email: decodedToken.email,
                        id: decodedToken.sid,
                        sub: decodedToken.sub,
                        name: decodedToken.name,
                        accessToken: res.data.accessToken
                    }
                }

                return null;
            }
        })
    ],

    session: {
        strategy: "jwt"
    },

    pages: {
        signIn: "/auth/login"
    },

    callbacks: {
        jwt: async ({ token, profile, account, user }) => {
            if (user?.name) {
                token.name = user.name;
            }
            if (user?.email) {
                token.email = user.email;
            }

            if(user?.accessToken){
                token.accessToken = user.accessToken
            }

            return token;
        },
        async session({ session, token, user }) {
            if (token) {
                session.user.name = token.name
                session.user.email = token.email
            }

            return { ...session, token: token };
        }
    }
})

export { handler as GET, handler as POST }
