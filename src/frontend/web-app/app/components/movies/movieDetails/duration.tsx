'use client'
import prettyMilliseconds from 'pretty-ms';
import { FaRegClock } from "react-icons/fa6";

interface Props {
    duration: number
}

export default function Duration({ duration }: Props) {
    return (
        <div className="flex flex-wrap">
            <div className="flex flex-row text-secondary text-1xl items-center mr-2">
                <FaRegClock />
                <span className="ml-1">{prettyMilliseconds(duration * 60_000, { compact: true, verbose: true })}</span>
            </div>
        </div >
    )
}