'use client'

import { useRouter } from "next/navigation";
import { FaArrowLeft } from "react-icons/fa";

export default function BackArrow() {
    const router = useRouter();

    function onSubmit() {
        router.back();
    }

    return (
        <a onClick={onSubmit} className="text-secondary hover:text-gray-500">
            <FaArrowLeft className="text-xl" />
        </a>
    )
}