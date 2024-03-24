import { Spinner } from 'flowbite-react';
import React from 'react';

interface Props {
    content?: string;
}

export default function LoadingComponent({ content = "Loading..." }: Props) {
    return (
        <div className="fixed top-0 left-0 w-full h-full flex items-center justify-center bg-black bg-opacity-50">
            <Spinner />
            <p className="text-white">{content}</p>
        </div>
    )
}