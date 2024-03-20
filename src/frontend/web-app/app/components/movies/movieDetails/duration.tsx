'use client'
import prettyMilliseconds from 'pretty-ms';

interface Props {
    duration: number
}

export default function Duration({ duration }: Props) {
    return (
        <div className="flex-wrap">
            <p className="text-gray-600">Duration: {prettyMilliseconds(duration * 60_000, { compact: true, verbose: true })}</p>
        </div>
    )
}