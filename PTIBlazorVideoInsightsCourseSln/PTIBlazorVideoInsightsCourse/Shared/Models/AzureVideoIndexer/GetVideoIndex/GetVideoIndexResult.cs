using System;
using System.Collections.Generic;

namespace PTIBlazorVideoInsightsCourse.Shared.Models.AzureVideoIndexer.GetVideoIndex
{
    public class Duration
    {
        /// <summary>
        /// 
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double seconds { get; set; }
    }

    public class AppearancesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string endTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double startSeconds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double endSeconds { get; set; }
    }

    public class KeywordsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public bool isTranscript { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AppearancesItem> appearances { get; set; }
    }

    public class SentimentsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string sentimentKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double seenDurationRatio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AppearancesItem> appearances { get; set; }
    }

    public class LabelsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AppearancesItem> appearances { get; set; }
    }

    public class FramePatternsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AppearancesItem> appearances { get; set; }
    }

    public class BrandsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string referenceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string referenceUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double confidence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double seenDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AppearancesItem> appearances { get; set; }
    }

    public class NamedLocationsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string referenceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string referenceUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double confidence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double seenDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AppearancesItem> appearances { get; set; }
    }

    public class Statistics
    {
        /// <summary>
        /// 
        /// </summary>
        public int correspondenceCount { get; set; }
    }

    public class TopicsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string referenceUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string iptcName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string iabName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double confidence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AppearancesItem> appearances { get; set; }
    }

    public class SummarizedInsights
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string privacyMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Duration duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thumbnailVideoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thumbnailId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<object> faces { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<KeywordsItem> keywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SentimentsItem> sentiments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<object> emotions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<object> audioEffects { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LabelsItem> labels { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FramePatternsItem> framePatterns { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<BrandsItem> brands { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<NamedLocationsItem> namedLocations { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<object> namedPeople { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Statistics statistics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TopicsItem> topics { get; set; }
    }

    public class InstancesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string adjustedStart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adjustedEnd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string end { get; set; }
    }

    public class TranscriptItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double confidence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int speakerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InstancesItem> instances { get; set; }
    }

    public class OcrItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double confidence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int left { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int top { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InstancesItem> instances { get; set; }
    }

    public class ScenesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InstancesItem> instances { get; set; }
    }

    public class KeyFramesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InstancesItem> instances { get; set; }
    }

    public class ShotsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<KeyFramesItem> keyFrames { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InstancesItem> instances { get; set; }
    }

    public class BlocksItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InstancesItem> instances { get; set; }
    }

    public class SpeakersItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InstancesItem> instances { get; set; }
    }

    public class TextualContentModeration
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int bannedWordsCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public int bannedWordsRatio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<object> instances { get; set; }
    }

    public class Insights
    {
        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceLanguage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<object> sourceLanguages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<object> languages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TranscriptItem> transcript { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OcrItem> ocr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<KeywordsItem> keywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TopicsItem> topics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LabelsItem> labels { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ScenesItem> scenes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ShotsItem> shots { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<BrandsItem> brands { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<NamedLocationsItem> namedLocations { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SentimentsItem> sentiments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<BlocksItem> blocks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FramePatternsItem> framePatterns { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SpeakersItem> speakers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TextualContentModeration textualContentModeration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Statistics statistics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double sourceLanguageConfidence { get; set; }
    }

    public class VideosItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string accountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string moderationState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reviewState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string privacyMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string processingProgress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string failureCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string failureMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string externalId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string externalUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string metadata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Insights insights { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thumbnailId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool detectSourceLanguage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string languageAutoDetectMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceLanguage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<object> sourceLanguages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<object> languages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string indexingPreset { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string linguisticModelId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string personModelId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isAdult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string publishedUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string publishedProxyUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string viewToken { get; set; }
    }

    public class Range
    {
        /// <summary>
        /// 
        /// </summary>
        public string start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string end { get; set; }
    }

    public class VideosRangesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string videoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Range range { get; set; }
    }

    public class GetVideoIndexResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string partition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string privacyMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string created { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isOwned { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isEditable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isBase { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double durationInSeconds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SummarizedInsights summarizedInsights { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<VideosItem> videos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<VideosRangesItem> videosRanges { get; set; }
    }
}
