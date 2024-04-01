import React, { ChangeEvent } from 'react';

interface ReviewFilterProps {
    onFilterChange: (rating: number | null) => void;
}

function ReviewFilters({ onFilterChange }: ReviewFilterProps) {

    const handleFilterChange = (event: ChangeEvent<HTMLSelectElement>) => {
        const rating = event.target.value ? parseInt(event.target.value) : null;
        onFilterChange(rating);
    };

    return (
        <div>
            <label htmlFor="rating-filter" className='text-secondary font-semibold mr-2'>Filter by stars:</label>
            <select id="rating-filter" onChange={handleFilterChange}
                className="rounded-full py-2 px-4 border-2 border-third bg-secondary bg-opacity-75 font-semibold text-header focus:border-third focus:ring-third">
                <option value="">All</option>
                <option value="1">1 Star</option>
                <option value="2">2 Stars</option>
                <option value="3">3 Stars</option>
                <option value="4">4 Stars</option>
                <option value="5">5 Stars</option>
            </select>
        </div>
    );
}

export default ReviewFilters;
