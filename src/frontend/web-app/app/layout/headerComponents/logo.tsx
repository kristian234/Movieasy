'use client'

import React from "react";
import { RiMovieLine } from "react-icons/ri";

export default function Logo() {
    return (
        <div className="flex items-center text-secondary">
            <RiMovieLine className="text-xl ml-2" />
            <span className="text-xl font-bold">Movieasy</span>
        </div>

    )
}