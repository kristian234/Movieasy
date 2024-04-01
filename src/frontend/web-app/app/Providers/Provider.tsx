'use client'

import { SessionProvider } from "next-auth/react";
import { ReactNode, useEffect } from "react";

interface Props {
    children: ReactNode;
}

export default function Provider({ children }: Props) {
    return <SessionProvider refetchOnWindowFocus={true} refetchInterval={60}>{children}</SessionProvider>
}