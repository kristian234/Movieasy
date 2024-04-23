import { DefaultSession } from "next-auth";

declare module 'next-auth' {
    interface Session {
        error?: "RefreshAccessTokenError",
        user: {
            id?: id,
            roles: string[],
            userId?: string | null
        } & DefaultSession['user']
    }

    interface User {
        accessToken?: string
        refreshToken?: string
        accessTokenExpiry?: number
        croles: string[],
        userId: string

    }
}

declare module 'next-auth/jwt' {
    interface JWT {
        accessToken?: string,
        refreshToken?: string,
        accessTokenExpiry?: number;
        croles: string[];
        userId: string;
        error?: "RefreshAccessTokenError"
    }
}

