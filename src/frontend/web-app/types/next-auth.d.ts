import { DefaultSession } from "next-auth";

declare module 'next-auth' {
    interface Session {
        error?: "RefreshAccessTokenError",
        user: {
            id?: id,
        } & DefaultSession['user']
    }

    interface User {
        accessToken?: string
        refreshToken?: string
        accessTokenExpiry?: number
    }
}

declare module 'next-auth/jwt' {
    interface JWT {
        accessToken?: string,
        refreshToken?: string,
        accessTokenExpiry?: number;
        error?: "RefreshAccessTokenError"
    }
}

