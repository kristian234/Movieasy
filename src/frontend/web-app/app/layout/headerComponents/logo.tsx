'use client'

import { useParamsStore } from "@/hooks/useParamsStore";
import React from "react";
import { RiMovieLine } from "react-icons/ri";

export default function Logo() {
    const reset = useParamsStore(state => state.reset);

    return (
        <a href="#">
            <div onClick={reset} className="flex items-center text-secondary">
                <RiMovieLine className="text-xl ml-2" />
                <span className="text-xl font-bold">Movieasy</span>
            </div>
        </a>
    )
}