'use client'

import Link from "next/link"
import { useRouter } from "next/navigation"
import { FaSignInAlt } from "react-icons/fa"

export default function LoginButton() {
    const router = useRouter();

    return (
        <div> 
            <button className="text-secondary" onClick={() => router.push('/auth/login')}>
                <FaSignInAlt className="mr-2" />
                Login
            </button>
        </div>
    )
}