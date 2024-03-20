'use client'

import Image from "next/image"
import { useState } from "react"

interface Props {
    src: string
}

export default function MovieImage({ src }: Props) {
    const [isLoading, setLoading] = useState(true);

    return (
        <div className="h-full w-full bg-secondary rounded-lg overflow-hidden">
            <Image
                src={src}
                alt="image"
                layout="fill"
                priority
                objectFit="cover"
                className={`object-cover rounded-2xl group-hover:opacity-75 duration-700 ease-in-out 
                ${isLoading ? 'grayscale blur-md scale-110' : 'grayscale-0 blur-0 scale-100'}
            `}
                onLoad={() => setLoading(false)}
            />
        </div>
    )
}