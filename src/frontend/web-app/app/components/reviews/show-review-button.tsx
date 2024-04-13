'use client'

import React, { useState } from 'react';
import ReviewListing from './review-listing';

interface Props {
    movieId: string
    isAdmin?: boolean | null
}

function ShowReviewsButton({ movieId, isAdmin = false}: Props) {
    const [showReviews, setShowReviews] = useState(false);

    const handleClick = () => {
        setShowReviews(!showReviews);
    };

    return (
        <div className='flex flex-col'>
            {!showReviews && (
                <button onClick={handleClick} className="flex items-center max-w-3xl justify-center mt-3 mx-auto text-secondary py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-semibold w-full bg-header hover:bg-darkHeader focus:outline-none focus:ring-1 focus:ring-secondary">
                    Show Reviews
                </button>
            )}

            {showReviews && <ReviewListing movieId={movieId} />}
        </div>
    );
}

export default ShowReviewsButton;
