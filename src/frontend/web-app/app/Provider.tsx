'use client'

import { SessionProvider, signOut, useSession } from "next-auth/react";
import { ReactNode, useEffect } from "react";

interface Props {
    children: ReactNode;
}

export default function Provider({ children }: Props) {
    
    return <SessionProvider refetchInterval={60}>{children}</SessionProvider>
}