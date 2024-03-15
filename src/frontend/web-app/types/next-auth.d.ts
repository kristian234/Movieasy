import { DefaultSession } from "next-auth";

declare module 'next-auth' {
    interface Session {
        error?: "RefreshAccessTokenError",
        user: {
            id?: id,
            roles: string[]
        } & DefaultSession['user']
    }

    interface User {
        accessToken?: string
        refreshToken?: string
        accessTokenExpiry?: number
        croles: string[]
    }
}

declare module 'next-auth/jwt' {
    interface JWT {
        accessToken?: string,
        refreshToken?: string,
        accessTokenExpiry?: number;
        croles: string[];
        error?: "RefreshAccessTokenError"
    }
}

