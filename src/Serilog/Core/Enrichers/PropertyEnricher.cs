// Copyright 2013-2015 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#nullable enable
using System;
using Serilog.Events;

namespace Serilog.Core.Enrichers
{
    /// <summary>
    /// Adds a new property enricher to the log event.
    /// </summary>
    public class PropertyEnricher : ILogEventEnricher
    {
        readonly string _name;
        readonly object? _value;
        readonly bool _destructureObjects;

        /// <summary>
        /// Create a new property enricher.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <param name="destructureObjects">If true, and the value is a non-primitive, non-array type,
        /// then the value will be converted to a structure; otherwise, unknown types will
        /// be converted to scalars, which are generally stored as strings.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="name"/> is <code>null</code></exception>
        /// <exception cref="ArgumentException">When <paramref name="name"/> is empty or only contains whitespace</exception>
        public PropertyEnricher(string name, object? value, bool destructureObjects = false)
        {
            LogEventProperty.EnsureValidName(name);

            _name = name;
            _value = value;
            _destructureObjects = destructureObjects;
        }

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="logEvent"/> is <code>null</code></exception>
        /// <exception cref="ArgumentNullException">When <paramref name="propertyFactory"/> is <code>null</code></exception>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (propertyFactory == null) throw new ArgumentNullException(nameof(propertyFactory));

            var property = propertyFactory.CreateProperty(_name, _value, _destructureObjects);
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}
