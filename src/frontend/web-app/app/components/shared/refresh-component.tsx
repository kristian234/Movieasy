'use client'

import { signOut, useSession } from "next-auth/react";
import { Fragment, ReactNode, useEffect } from "react";

interface Props {
    children: ReactNode;
}

export default function RefreshClientComponent({children} : Props) {
    const { data: session } = useSession();

    useEffect(() => {
        if (session?.error === "RefreshAccessTokenError") {
            console.log("BREOAKFKOAEKOEA" + session?.error)
            signOut(); // Force sign in to hopefully resolve error
        }
    }, [session]);

    return(
        <div>
            {children}
        </div>
    )
}