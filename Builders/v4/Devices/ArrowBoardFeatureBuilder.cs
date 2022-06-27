using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// Provides an immutable builder of a v4 FieldDeviceFeature (ArrowBoard) class
    /// </summary>
    public class ArrowBoardFeatureBuilder : FieldDeviceFeatureBuilder<ArrowBoardFeatureBuilder, ArrowBoard>
    {
        public ArrowBoardFeatureBuilder(string sourceId, string featureId, string roadName) :
            this(new List<Action<FieldDeviceFeature>>(), (feature, properties) =>
            {
                var geometry = Point.FromCoordinates(Position.From(0, 0));
                feature.Id = featureId;
                feature.Geometry = geometry;
                feature.BoundaryBox = geometry.BoundaryBox.ToList().AsReadOnly();
                
                properties.CoreDetails.DataSourceId = sourceId;
                properties.CoreDetails.RoadNames.Add(roadName);
                properties.CoreDetails.DeviceStatus = FieldDeviceStatus.Unknown;
            })
        {
            // ignore
        }

        private ArrowBoardFeatureBuilder(IEnumerable<Action<FieldDeviceFeature>> configuration, Action<FieldDeviceFeature, ArrowBoard> step) : 
            base(configuration, step)
        {
            // ignore
        }

        [Pure]
        public ArrowBoardFeatureBuilder WithIsMoving(bool value)
        {
            return CreateWith((feature, board) => board.IsMoving = value);
        }

        [Pure]
        public ArrowBoardFeatureBuilder WithIsInTransportPosition(bool value)
        {
            return CreateWith((feature, board) => board.IsInTransportPosition = value);
        }

        [Pure]
        public ArrowBoardFeatureBuilder WithPattern(ArrowBoardPattern value)
        {
            return CreateWith((feature, board) => board.Pattern = value);
        }

        protected override ArrowBoardFeatureBuilder CreateWith(Action<FieldDeviceFeature, ArrowBoard> step)
        {
            return new ArrowBoardFeatureBuilder(Configuration, step);
        }

        protected override Func<ArrowBoard> ResultProperties { get; } = () => new ArrowBoard();
    }
}