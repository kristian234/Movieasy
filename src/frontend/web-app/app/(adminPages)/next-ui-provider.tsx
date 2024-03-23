'use client'

import { NextUIProvider } from "@nextui-org/react";
import { ReactNode } from "react";

interface Props {
    children: ReactNode;
}

export default function NextProvider({ children }: Props) {
    return <NextUIProvider>{children}</NextUIProvider>
}