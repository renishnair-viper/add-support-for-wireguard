﻿// <copyright file="LocationsComparer.cs" company="StackPath, LLC">
// Copyright (c) StackPath, LLC. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using DynamicData.Binding;
using VpnSDK.Interfaces;

namespace Example.Helpers
{
    /// <summary>
    /// LocationsComparer class. Compares two ILocation.
    /// </summary>
    public class LocationsComparer : List<SortExpression<ILocation>>, IComparer<ILocation>
    {
        /// <summary>
        /// Creates new LocationsComparer with <see cref="SortDirection.Ascending" /> sort direction.
        /// </summary>
        /// <param name="expression">the expression.</param>
        /// <returns><see cref="LocationsComparer"/> instance.</returns>
        public static LocationsComparer Ascending(Func<ILocation, IComparable> expression)
        {
            return new LocationsComparer { new SortExpression<ILocation>(expression) };
        }

        /// <summary>
        /// Creates new LocationsComparer with <see cref="SortDirection.Descending" /> sort direction.
        /// </summary>
        /// <param name="expression">the expression.</param>
        /// <returns><see cref="LocationsComparer"/> instance.</returns>
        public static LocationsComparer Descending(Func<ILocation, IComparable> expression)
        {
            return new LocationsComparer { new SortExpression<ILocation>(expression, SortDirection.Descending) };
        }

        /// <summary>
        /// Compares two ILocation objects
        /// </summary>
        /// <param name="x">First location.</param>
        /// <param name="y">Second location.</param>
        /// <returns>Comparison result.</returns>
        public int Compare(ILocation x, ILocation y)
        {
            foreach (var item in this)
            {
                if (x == null && y == null)
                {
                    continue;
                }

                if (x == null)
                {
                    return -1;
                }

                if (y == null)
                {
                    return 1;
                }

                var xValue = item.Expression(x);
                var yValue = item.Expression(y);

                if (xValue == null && yValue == null)
                {
                    continue;
                }

                if (xValue == null)
                {
                    return -1;
                }

                if (yValue == null)
                {
                    return 1;
                }

                int result = xValue.CompareTo(yValue);
                if (result == 0)
                {
                    continue;
                }

                return (item.Direction == SortDirection.Ascending) ? result : -result;
            }

            return 0;
        }

        /// <summary>
        /// Adds <see cref="SortDirection.Ascending"/> sort direction.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns><see cref="LocationsComparer"/> instance.</returns>
        public LocationsComparer ThenByAscending(Func<ILocation, IComparable> expression)
        {
            Add(new SortExpression<ILocation>(expression));
            return this;
        }

        /// <summary>
        /// Adds <see cref="SortDirection.Descending"/> sort direction.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns><see cref="LocationsComparer"/> instance.</returns>
        public LocationsComparer ThenByDescending(Func<ILocation, IComparable> expression)
        {
            Add(new SortExpression<ILocation>(expression, SortDirection.Descending));
            return this;
        }
    }
}