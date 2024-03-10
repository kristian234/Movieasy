'use client'

import { useSession, signIn, signOut } from "next-auth/react";
import { useEffect } from "react";
import Body from "../layout/body";

export default function AppContainer() {
    const { data: session } = useSession();

    useEffect(() => {
        if (session?.error === "RefreshAccessTokenError") {
            //console.log("BREOAKFKOAEKOEA" + session?.error)
            signOut(); // Force sign in to hopefully resolve error
        }
    }, [session])
    return (
        <div>
            <Body />
        </div>
    )
}