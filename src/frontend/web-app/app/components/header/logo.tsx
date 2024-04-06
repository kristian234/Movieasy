'use client'

import { useParamsStore } from "@/hooks/useParamsStore";
import Link from "next/link";
import { useRouter } from "next/navigation";
import React from "react";
import { RiMovieLine } from "react-icons/ri";

export default function Logo() {
    //const reset = useParamsStore(state => state.reset);
    const router = useRouter();
    return (
        <Link href='/'>
            <div className="flex items-center text-secondary">
                <RiMovieLine className="text-xl ml-2" />
                <span className="text-xl font-bold">Movieasy</span>
            </div>
        </Link>
    )
}