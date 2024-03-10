'use client'

import { SessionProvider, signOut, useSession } from "next-auth/react";
import { ReactNode, useEffect } from "react";

interface Props {
    children: ReactNode;
}

export default function Provider({ children }: Props) {
    const { data: session } = useSession();

    useEffect(() => {
        if (session?.error === "RefreshAccessTokenError") {
            //console.log("BREOAKFKOAEKOEA" + session?.error)
            signOut(); // Force sign in to hopefully resolve error
        }
    }, [session]);

    return <SessionProvider>{children}</SessionProvider>
}