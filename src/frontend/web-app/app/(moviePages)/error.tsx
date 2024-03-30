'use client';

import { useEffect } from 'react';

export default function Error({
    error,
    reset,
}: {
    error: Error & { digest?: string };
    reset: () => void;
}) {
    useEffect(() => {

    }, [error]);

    return (
        <main className="flex h-full flex-col items-center justify-center">
            <div className="mt-8 bg-black bg-opacity-15 shadow-3xl max-w-4xl p-8 rounded-lg">
                <h1 className="text-center font-bold text-primary">Something went wrong!</h1>
                {error && (
                    <p className="text-center text-gray-600 mt-4">
                        An unexpected error occurred. Please try again.
                    </p>
                )}
                <div className='justify-center items-center flex'>
                    <button
                        className="mt-6 font-semibold rounded-md items-center justify-center bg-secondary px-4 py-2 text-sm text-primary transition-colors hover:bg-third"
                        onClick={() => reset()}
                    >
                        Try again
                    </button>
                </div>
            </div>
        </main>
    );
}