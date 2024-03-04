'use client'

import { FaSignInAlt } from "react-icons/fa"

export default function LoginButton() {
    return (
        <div>
            <button className="text-secondary flex items-center">
                <FaSignInAlt className="mr-2" />
                Login
            </button>
        </div>
    )
}