'use client'

import { signOut, useSession } from "next-auth/react";
import { Fragment, useEffect } from "react";

export default function RefreshClientComponent() {
    const { data: session } = useSession();

    useEffect(() => {
        if (session?.error === "RefreshAccessTokenError") {
            //console.log("BREOAKFKOAEKOEA" + session?.error)
            signOut(); // Force sign in to hopefully resolve error
        }
    }, [session]);

    return(
        <Fragment>
            
        </Fragment>
    )
}
