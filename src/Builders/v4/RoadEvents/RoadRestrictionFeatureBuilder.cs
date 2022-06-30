using System;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides a builder for a v4 RoadEventFeature (Restriction) class
    /// </summary>
    public sealed class RoadRestrictionFeatureBuilder : RoadEventFeatureBuilder<RoadRestrictionFeatureBuilder, RestrictionRoadEvent>
    {
        public RoadRestrictionFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction) :
            base(new DelegatingFactory<RoadEventFeature>(() => new RoadEventFeature() { Properties = new RestrictionRoadEvent() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(MultiPoint.FromCoordinates(Enumerable.Empty<Position>()));
            WithRoadName(roadName);
            WithDirection(direction);
        }

        public RoadRestrictionFeatureBuilder WithLane(LaneType type, LaneStatus status, int order, Func<LaneBuilder, LaneBuilder> configure)
        {
            var builder = configure(new LaneBuilder(type, status, order));
            var lane = builder.Result();
            PropertiesConfiguration.Combine(properties => properties.Lanes, properties => properties.Lanes.Add(lane));
            return Derived();
        }

        public RoadRestrictionFeatureBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Func<RestrictionBuilder, RestrictionBuilder> configure)
        {
            var builder = configure(new RestrictionBuilder(type, unit));
            var restriction = builder.Result();
            PropertiesConfiguration.Combine(properties => properties.Restrictions, properties => properties.Restrictions.Add(restriction));
            return Derived();
        }
    }
}