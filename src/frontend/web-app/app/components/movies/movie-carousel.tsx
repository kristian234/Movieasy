'use client'

import { Movie, PagedResult } from '@/types';
import React, { useEffect, useState } from "react";
import MovieCard from './movie-card';
import Carousel from 'react-multi-carousel';
import LoadingComponent from '../shared/loading-component';
import 'react-multi-carousel/lib/styles.css';

interface Props {
    movies: () => Promise<PagedResult<Movie>>
    text: string
}

const responsive = {
    desktop: {
        breakpoint: { max: 3000, min: 1024 },
        items: 6,
        slidesToSlide: 2 // optional, default to 1.
    },
    tablet: {
        breakpoint: { max: 1024, min: 464 },
        items: 3,
        slidesToSlide: 2 // optional, default to 1.
    },
    mobile: {
        breakpoint: { max: 464, min: 0 },
        items: 2,
        slidesToSlide: 1 // optional, default to 1.
    }
}

export default function MovieCarousel({ movies, text }: Props) {
    const [data, setData] = useState<PagedResult<Movie>>();
    const [loading, setLoading] = useState<boolean>(true); 

    useEffect(() => {
        movies()
            .then(data => {
                setData(data);
                setLoading(false);
            })
            .catch(error => {
                console.error("Error loading movies:", error);
                setLoading(false); 
            });
    }, [movies])

    if (loading) {
        return <LoadingComponent />; 
    }

    if (!data?.items) {
        return null;
    }

    return (
        <div>
            <div className="p-2 w-[90%] ssm:w-[70%] mx-auto">
                <h1 className="font-bold text-secondary ml-3">{text}:</h1>
                <div className="items-center justify-center">
                    <Carousel
                        responsive={responsive}
                        additionalTransfrom={0}
                        arrows
                        autoPlaySpeed={2000}
                        centerMode={false}
                        className=""
                        containerClass="container-with-dots"
                        dotListClass=""
                        draggable
                        focusOnSelect={false}
                        infinite
                        itemClass="carousel-item-padding-50-px"
                        keyBoardControl
                        minimumTouchDrag={80}
                        pauseOnHover
                        renderArrowsWhenDisabled={false}
                        renderButtonGroupOutside={false}
                        renderDotsOutside={false}
                        rewind={false}
                        rewindWithAnimation={false}
                        shouldResetAutoplay
                        showDots={false}
                        sliderClass=""
                        swipeable>
                        {data.items.map((movie, index) => (
                            <div key={index}>
                                <MovieCard
                                    isCarousel={true}
                                    movie={movie} />
                            </div>
                        ))}
                    </Carousel>
                </div>
            </div>
        </div>
    );
}